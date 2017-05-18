using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// uses origin and adds to x and y
// scale - 1 city block = 1 unity unit
public class CityGeneration : MonoBehaviour {

  [Header("The Current City")]
  public List<Transform> currentCity;

  [Header("Prefab List Here")]
  public List<Transform> cityBlockPrefabs;

  [Header("Generation Options")]
  [Header("Number Of Blocks to Generate")]
  public int cityBlocksCountWidth;// x
  public int cityBlocksCountDepth;// z

  [Header("Width of Each Block")]
  public float blockSize = 1.0f; 
  public float roadSize = 0.2f;// not added yet

  [Header("Additional Options")]
  public bool generateOnStart = true;
  public bool centerCity = true;
  public Transform parentTransform; // if you wnat a different parent. otherwise uses this one

  // the max width and depth of the city
  private float maxWidth = 0; // x
  private float maxDepth = 0; // z

  private Vector3 currentSpawnLocation = Vector3.zero;

	// Use this for initialization
	void Start () {
    if( generateOnStart){
      GenerateCity();
    }
	}
	
  void GenerateCity(){
    if( parentTransform == null){
      parentTransform = transform;
    }

    Transform currentTempBlock;

    for( int i=0; i< cityBlocksCountWidth; i++ ){
      for( int j=0; j< cityBlocksCountDepth; j++ ){

        currentTempBlock = (Transform) Instantiate(GetRandomBlock(true), currentSpawnLocation, GetRandomRotation());
        currentTempBlock.gameObject.name = "CityBlock["+i+"_"+j+"]_" + currentTempBlock.gameObject.name;

        currentTempBlock.SetParent(parentTransform);

        currentCity.Add(currentTempBlock);
      
        // update pointer
        currentSpawnLocation.z += blockSize;

      }

      currentSpawnLocation.z = 0;
      currentSpawnLocation.x += blockSize;
    }
  
    // figure out bounds and offset city to be centered
    maxWidth = cityBlocksCountWidth * blockSize;
    maxDepth = cityBlocksCountDepth * blockSize;

    if( centerCity ){
      parentTransform.Translate( maxWidth * -0.5f, maxDepth * -0.5f, 0);
    }
  
  }


  // Get a random city block from the list
  private Transform GetRandomBlock(bool rotateRandom){
    
    Transform tempBlock;

    // get random index of city block list
    int randomIndex = Random.Range(0,cityBlockPrefabs.Count);

    // grab tempBlock
    tempBlock = cityBlockPrefabs[randomIndex];

    // error checking
    if(tempBlock == null){
      Debug.LogWarning("GetRandomBlock Failed to generate a block");
    }

    return tempBlock;
  }

  // get a random 90* offset rotation
  private Quaternion GetRandomRotation(){
    //randomize rotation
    Quaternion returnValue;
    Vector3 randomRotation = Vector3.zero;
    int rotate = Random.Range(0,3);
    randomRotation.y = rotate * 90.0f;

    returnValue = Quaternion.Euler(randomRotation);
    return returnValue;
  }

	// Update is called once per frame
	void Update () {
		
	}
}
