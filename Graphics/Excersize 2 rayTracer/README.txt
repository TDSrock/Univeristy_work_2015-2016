Assigment 2 - Raytracer - Graphics - UU 2016

Controls:
Camera movement - WASD
Camera tilting - IKJL
Camera up down - QE
Field of view - UO
Anti-aliasing - MN

(A) Made by:
Cas Laugs - (4140613)
Sjors Gielen - (5558956)
Frans Zoetmulder - (5729378)

(B) Bonus assigments:
The following bonus assigments were implemented in our raytracer:
	- Absorption (1pt)
	- Anti-aliasing (1pt)
	- Multithreading (???)

Absorption, reflective materials in our raytracer have a absorption value. In the  delivered version
The left sphere has a absorption value of 1, the middle has a value of 1 (but it is fully reflective)
and the right sphere has a value of 0.3.

Anti-aliasing, anti-aliasing was achieved by casting extra rays with small offsets for each pixel and using the average.
The base value of anti-aliasing = 1, that means that there are no extra rays casted. Using the M key, the value can be
increased and it can be lowered with the N-key.

That is a copy of the AA code:
for(float yaa = y; yaa < 1 + y; yaa += stepSize)
	for (float xaa = x; xaa < 1 + x; xaa += stepSize)
	{
		if(yaa == 256 && xaa % 32 == 0)
		{
			colour += GenerateRay(xaa, yaa, true);
		}
		else
		{
			colour += GenerateRay(xaa, yaa);
		}
		 
	}

raytracerScreen.Plot(x, (512 - y), CreateRGB((int)(colour.X / (AA * AA) * 255), (int)(colour.Y / (AA * AA) * 255),
(int)(colour.Z / (AA * AA) * 255)));

Mutlithreading, multithreading was also implemented to increase the performance of the raytracer. The number of 
threads is the amount of processor times two.

(C) List of materials
No materials were used aside from studying the slides and information posted on slack.