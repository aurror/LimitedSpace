using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TravelManager : MonoBehaviour
{
        private bool finished = false;
        public RectTransform progressBar;
        private float targetDistance = 60*2.5f; // 2,5Minutes
        private float percentProgress = 0f;
        private int maxLength = 450;
        private float traveledDistance = 0f;
        private float currentTravelSpeed = 0f;
        private float goodTravelDistance = 1f;

        private float damagedTravelDistance = .90f;
        private float badTravelDistance = .30f;
        private float badderTravelDistance = 0f;

        public GameObject EndScreen;

        void Update(){
                if (!finished){
                traveledDistance += Time.deltaTime * currentTravelSpeed;
                percentProgress = traveledDistance / targetDistance;
                if(percentProgress > 1f){
                        percentProgress = 1f;
                }
                progressBar.localScale = new Vector3(maxLength * percentProgress,1,1);
                if(traveledDistance >= targetDistance){
                        // CONGRATIONALS YOU DID IT
                        finished = true;
                        GameObject.Find("Player").GetComponent<Animator>().SetTrigger("PlayerWin");
                        Debug.Log("🍀🍀🍀FUCK YEAH YOU DID IT YOU BEAUTIFUL SHINING HUMAN🍀🍀🍀");
                        //EndScreen.SetActive(true);
                }
        }

        }
        public void SetHealthy(){
                currentTravelSpeed = goodTravelDistance;
        }

        public void SetDamaged(){
                currentTravelSpeed = damagedTravelDistance;
        }

        public void SetKaputt(){
                currentTravelSpeed = badTravelDistance;
        }

        public void SetFuckedUp(){
                currentTravelSpeed = badderTravelDistance;
        }
}