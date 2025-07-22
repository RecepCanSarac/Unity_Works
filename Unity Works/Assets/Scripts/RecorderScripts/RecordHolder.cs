using System.Collections.Generic;
using UnityEngine;

public class RecordHolder : MonoBehaviour
{
    public float rewindDuration = 2f;
    public float rewindSpeed = 1f;    

    private List<ReplayRecordSaver> replayRecord = new List<ReplayRecordSaver>();
    private Rigidbody rootRigidbody;

    private bool isRewinding = false;
    private int rewindIndex = 0;
    private int targetIndex = 0;
    private float rewindTimer = 0f;


    public GameObject canvasUI;

    private void Awake()
    {
        rootRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRewinding)
        {
            StartRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            rewindTimer -= Time.fixedDeltaTime;

            if (rewindIndex > targetIndex && rewindIndex > 0)
            {
                SetTransform(replayRecord[rewindIndex]);
                rewindIndex--;
            }
            else
            {
                StopRewind();
            }
        }
        else
        {
            replayRecord.Add(new ReplayRecordSaver
            {
                position = transform.position,
                rotation = transform.rotation
            });

            float recordTimeLimit = rewindDuration + 1f;
            int maxRecordCount = Mathf.CeilToInt(recordTimeLimit / Time.fixedDeltaTime);

            if (replayRecord.Count > maxRecordCount)
                replayRecord.RemoveAt(0);
        }
    }

    private void StartRewind()
    {
        canvasUI.SetActive(true);
        isRewinding = true;
        rootRigidbody.isKinematic = true;

        rewindTimer = rewindDuration;

        rewindIndex = replayRecord.Count - 1;
        targetIndex = Mathf.Max(0, replayRecord.Count - Mathf.CeilToInt(rewindDuration / Time.fixedDeltaTime));
    }

    private void StopRewind()
    {
        isRewinding = false;
        rootRigidbody.isKinematic = false;
        canvasUI.SetActive(false);
    }

    private void SetTransform(ReplayRecordSaver data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
    }
}
