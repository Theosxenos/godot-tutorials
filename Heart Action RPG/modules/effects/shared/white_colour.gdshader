shader_type canvas_item;

uniform bool Active = false;

void fragment() {
	vec4 previousColour = texture(TEXTURE, UV);
	vec4 whiteColour = vec4(1,1,1,previousColour.a);
	vec4 newColour = previousColour;
	if(Active == true) {
		newColour = whiteColour;
	}
	COLOR = newColour;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
