using Obi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.UI.Image;

public class RopeManager : MonoBehaviour
{
    public ObiSolver solver;
    public List<ObiCollider> characters = new List<ObiCollider>();

    public Material material;
    public ObiRopeSection section;

    [Range(0.1f, 100)]
    public float maxDistanceBetweenCharacters = 10;

    private int particlePoolSize = 100;

    private ObiRope rope;
    private ObiRopeBlueprint blueprint;
    private ObiRopeExtrudedRenderer ropeRenderer;

    private ObiRopeCursor cursor;

    void Awake()
    {

        // Create both the rope and the solver:	
        rope = gameObject.AddComponent<ObiRope>();
        ropeRenderer = gameObject.AddComponent<ObiRopeExtrudedRenderer>();
        ropeRenderer.section = section;
        ropeRenderer.uvScale = new Vector2(1, 4);
        ropeRenderer.normalizeV = false;
        ropeRenderer.uvAnchor = 1;
        rope.GetComponent<MeshRenderer>().material = material;

        // Setup a blueprint for the rope:
        blueprint = ScriptableObject.CreateInstance<ObiRopeBlueprint>();
        blueprint.resolution = 1f;
        blueprint.pooledParticles = particlePoolSize;

        // Tweak rope parameters:
        rope.maxBending = 0.02f;

        // Add a cursor to be able to change rope length:
        cursor = rope.gameObject.AddComponent<ObiRopeCursor>();
        cursor.cursorMu = 0;
        cursor.direction = true;

        CreateRope();
    }

    void LateUpdate()
    {
        CheckIfRopeBreaks();
    }

    private void CreateRope()
    {
        StartCoroutine(CreateRopeCoroutine());
    }

    private IEnumerator CreateRopeCoroutine()
    {
        yield return null;

        // Clear pin constraints:
        var pinConstraints = rope.GetConstraintsByType(Oni.ConstraintType.Pin) as ObiConstraints<ObiPinConstraintsBatch>;
        pinConstraints.Clear();

        Vector3 ropeStartAttachement = rope.transform.InverseTransformPoint(characters[0].transform.position);
        Vector3 ropeEndAttachement = rope.transform.InverseTransformPoint(characters[1].transform.position);

        // Procedurally generate the rope path (just a short segment, as we will extend it over time):
        int filter = ObiUtils.MakeFilter(ObiUtils.CollideWithEverything, 0);
        blueprint.path.Clear();
        blueprint.path.AddControlPoint(ropeStartAttachement, Vector3.zero, Vector3.zero, Vector3.up, 0.1f, 0.1f, 1, filter, Color.white, "Start");
        blueprint.path.AddControlPoint(ropeEndAttachement.normalized * 0.5f, Vector3.zero, Vector3.zero, Vector3.up, 0.1f, 0.1f, 1, filter, Color.white, "End");
        blueprint.path.FlushEvents();

        // Generate the particle representation of the rope (wait until it has finished):
        yield return blueprint.Generate();

        // Set the blueprint (this adds particles/constraints to the solver and starts simulating them).
        rope.ropeBlueprint = blueprint;

        // wait one frame:
        yield return null;

        rope.GetComponent<MeshRenderer>().enabled = true;

        // set masses to zero, as we're going to override positions while we extend the rope:
        for (int i = 0; i < rope.activeParticleCount; ++i)
            solver.invMasses[rope.solverIndices[i]] = 0;

        float currentLength = 0;

        // while the last particle hasn't reached the hit, extend the rope:
        while (true)
        {
            // calculate rope origin in solver space:
            Vector3 origin = solver.transform.InverseTransformPoint(rope.transform.position);

            // update direction and distance to hook point:
            Vector3 direction = characters[1].transform.position - origin;
            float distance = direction.magnitude;
            direction.Normalize();

            // increase length:
            currentLength += 100 * Time.deltaTime;

            // if we have reached the desired length, break the loop:
            if (currentLength >= distance)
            {
                cursor.ChangeLength(distance);
                break;
            }

            // change rope length (clamp to distance between rope origin and hook to avoid overshoot)
            cursor.ChangeLength(Mathf.Min(distance, currentLength));

            // iterate over all particles in sequential order, placing them in a straight line while
            // respecting element length:
            float length = 0;
            for (int i = 0; i < rope.elements.Count; ++i)
            {
                solver.positions[rope.elements[i].particle1] = origin + direction * length;
                solver.positions[rope.elements[i].particle2] = origin + direction * (length + rope.elements[i].restLength);
                length += rope.elements[i].restLength;
            }

            // wait one frame:
            yield return null;
        }

        // restore masses so that the simulation takes over now that the rope is in place:
        for (int i = 0; i < rope.activeParticleCount; ++i)
            solver.invMasses[rope.solverIndices[i]] = 10; // 1/0.1 = 10

        // Pin both ends of the rope (this enables two-way interaction between character and rope):
        var batch = new ObiPinConstraintsBatch();
        batch.AddConstraint(rope.elements[0].particle1, characters[0],
                                                          characters[0].gameObject.GetComponent<Collider>().transform.InverseTransformPoint(characters[0].transform.position), Quaternion.identity, 0, 0, float.PositiveInfinity);
        batch.AddConstraint(rope.elements[rope.elements.Count - 1].particle2, characters[1],
                                                          characters[1].gameObject.GetComponent<Collider>().transform.InverseTransformPoint(characters[1].transform.position), Quaternion.identity, 0, 0, float.PositiveInfinity);
        batch.activeConstraintCount = 2;
        pinConstraints.AddBatch(batch);

        rope.SetConstraintsDirty(Oni.ConstraintType.Pin);
    }

    private void CheckIfRopeBreaks()
    {
        float distance = (characters[1].transform.position - characters[0].transform.position).magnitude;

        if (distance >= maxDistanceBetweenCharacters)
        {
            int middleOfRopeIndex = Mathf.FloorToInt(rope.elements.Count / 2);
            rope.Tear(rope.elements[middleOfRopeIndex]);
            rope.RebuildConstraintsFromElements();
            GameManager.Instance.UpdateGameState(GameState.GameOver);
        }
    }
}
