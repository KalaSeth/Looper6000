------------ How to Import 3D models ------------

These assets were created with the Unity engine in mind. Though they
can be used in other engines, the frontal axis for the 3D Models will be pointing the wrong way.

1. Import the models and the textures "T_GameJamCP_AL" & "T_GameJamCP_E" into Unity by dragging them into the Unity
Assets folder.

2. Make sure that both the texture have Compression set to "None" and Filter Mode set to "Point (no filter)" 
and that under "Advanced" the "Non-Power of 2" setting is set to "None".
These settings can be found when clicking directly on the Textures in Unity.

3. Create a material anywhere in your Asset folder by right clicking and selecting "Create -> Material". 
Click directly on the material to access it's settings.
"T_GameJamCP_AL" goes into the "Albedo" slot. The "T_GameJamCP_E" goes into the "Emission" slot.
Some models have intentional white coloring so you can manually pick the color in the material color selector.

4. Drag any model into the scene hierarchy.

5. Drag a Material directly onto the model in Scene view or in the hierarchy.



