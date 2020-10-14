using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class FootstepSounds : TFPExtension{
    
    public float distanceBetweenFootsteps;
    public AudioSource leftFoot, rightFoot;
    public FootstepGroup[] footsteps;

    public FootstepGroup defaultFootsteps;
    float leftDistance, rightDistance;
    public float stepTimeGap;
    float nextsteptime;

    RaycastHit groundTypeCheck;

    public override void ExStart(ref TFPData data, TFPInfo info){
        leftDistance = 0;
        rightDistance = distanceBetweenFootsteps;
    }

    public override void ExPostUpdate(ref TFPData data, TFPInfo info){
        if(data.moving && data.grounded){
            float deltaMove = Vector3.Scale(data.lastMove,new Vector3(1,0,1)).magnitude * Time.deltaTime;
            leftDistance -= deltaMove;
            rightDistance -= deltaMove;
            if(leftDistance <= 0 && Time.time > nextsteptime)
            {
                leftDistance += distanceBetweenFootsteps*2;
                leftFoot.PlayOneShot(GetClip(info));
                nextsteptime = Time.time + stepTimeGap;
            }
            if(rightDistance <= 0 && Time.time > nextsteptime)
            {
                rightDistance += distanceBetweenFootsteps*2;
                rightFoot.PlayOneShot(GetClip(info));
                nextsteptime = Time.time + stepTimeGap;
            }
        }else{
            leftDistance = 0;
            rightDistance = distanceBetweenFootsteps;
        }
        
    }

    AudioClip GetClip(TFPInfo info){
        if(Physics.SphereCast(transform.position+(Vector3.up*info.controller.radius),info.controller.radius,Vector3.down,out groundTypeCheck,info.crouchHeadHitLayerMask.value)){
            TerrainCollider hitTerrain = groundTypeCheck.transform.GetComponent<TerrainCollider>();
            MeshRenderer hitMesh = groundTypeCheck.transform.GetComponent<MeshRenderer>();
            Texture2D hitTexture;
            if(hitTerrain != null){
                hitTexture = TerrainSurface.GetMainTexture(groundTypeCheck.transform.GetComponent<Terrain>(), transform.position);
            }else if(hitMesh != null){
                hitTexture = hitMesh.material.mainTexture as Texture2D;
            }else{
                return defaultFootsteps.footSounds[Random.Range(0,defaultFootsteps.footSounds.Length)];
            }
            //print(hitTexture);
            foreach(FootstepGroup fsGroup in footsteps){
                foreach(Texture2D tex in fsGroup.textures){
                    if(hitTexture == tex){
                        return fsGroup.footSounds[Random.Range(0,fsGroup.footSounds.Length)];
                    }
                }
            }
        }
        return defaultFootsteps.footSounds[Random.Range(0,defaultFootsteps.footSounds.Length)];
    }
}
