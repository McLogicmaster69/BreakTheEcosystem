using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class RGBColor : MonoBehaviour
    {
        [SerializeField] private Material Material;

        private float RVal = 1f;
        private float GVal = 0f;
        private float BVal = 0f;

        private void Update()
        {
            if(RVal == 1f)
            {
                if(GVal == 1f)
                {
                    RVal -= Time.deltaTime;
                    if (RVal < 0f)
                        RVal = 0f;
                }
                else if(BVal > 0f)
                {
                    BVal -= Time.deltaTime;
                    if (BVal < 0f)
                        BVal = 0f;
                }
                else
                {
                    GVal += Time.deltaTime;
                    if (GVal > 1f)
                        GVal = 1f;
                }
            }
            else if(GVal == 1f)
            {
                if(BVal == 1f)
                {
                    GVal -= Time.deltaTime;
                    if (GVal < 0f)
                        GVal = 0f;
                }
                else if(RVal > 0f)
                {
                    RVal -= Time.deltaTime;
                    if (RVal < 0f)
                        RVal = 0f;
                }
                else
                {
                    BVal += Time.deltaTime;
                    if (BVal > 1f)
                        BVal = 1f;
                }
            }
            else if(BVal == 1f)
            {
                if (RVal == 1f)
                {
                    BVal -= Time.deltaTime;
                    if (BVal < 0f)
                        BVal = 0f;
                }
                else if (GVal > 0f)
                {
                    GVal -= Time.deltaTime;
                    if (GVal < 0f)
                        GVal = 0f;
                }
                else
                {
                    RVal += Time.deltaTime;
                    if (RVal > 1f)
                        RVal = 1f;
                }
            }
            else
            {
                RVal = 1f;
                GVal = 0f;
                BVal = 0f;
            }

            Material.color = new Color(RVal, GVal, BVal);
        }
    }
}