/*
* Copyright (C) Ari Mahardika Ahmad Nafis, All Right Reserved
* Proprietary and confidential
* Written by Ari Mahardika Ahmad Nafis, <arimahardika.an@gmail.com>, April 2020

* File name                 : ARPreprocessing
* Developed on              : 29/04/2020
* Description               : This script is used for easier time while pre-processing 3D model before used in AR projects.
                              Function :
                              - Unpack prefab completely
                              - Delete "Camera" inside the prefab.
                              - Delete all external light source inside prefab.

* Developed by              : Ari Mahardika Ahmad Nafis
* Contact                   : arimahardika.an@gmail.com
* Used in (Unity only)      : Assets/Editor

* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.

* UNAUTHORIZED COPYING OF THIS FILE, OR PART OF IT, VIA ANY MEDIUM IS STRICLY PROHIBITED
*/

using UnityEngine;
using UnityEditor;

public class ARPreprocessing
{
    [MenuItem("AR Preprocessing/Process Prefab %u")]
    public static void Unpack(){
        //Check if selecting anything
        if(!Selection.activeGameObject) return;

        //Get selected object
        GameObject selected = Selection.activeGameObject;
        
        //Unpack prefab completely
        if(PrefabUtility.IsAnyPrefabInstanceRoot(selected)){
            PrefabUtility.UnpackPrefabInstance(
                selected, 
                PrefabUnpackMode.Completely, 
                UnityEditor.InteractionMode.AutomatedAction);
        };

        //Find the camera and delete it
        GameObject child_camera = selected.transform.Find("Camera").gameObject;
        UnityEngine.Object.DestroyImmediate(child_camera);

        //Find the external light source and delete it
        var lightSource = UnityEngine.Object.FindObjectsOfType<Light>();
        for (int i = 0; i < lightSource.Length; i++){
            //Check if parent of light is the selected prefab
            if(lightSource[i].transform.root.name == selected.name){
                UnityEngine.Object.DestroyImmediate(lightSource[i].gameObject);
            }
        }

        //Preprocessing Complete
        Debug.Log(selected.name + "'s preprocess is done");
    }
}