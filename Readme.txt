Kosta Assignment project for Unity developer position at Candivore 

Hi
I hope i didint go too far from the scope of the assignment
but i wanted to create something fun and unique 
something that i will enjoy making and you will enjoy playing
so i desided to make a this Pinata game a platformer with a character that can move and shoot
the goal is to destroy the pinata befor time is up and collect the drops at the same time.

Techncal details:

Design Pattern- i desided to do with a simple approach, i relay on events, a lot of the scripts are pure C# non monobehaviour.
i used ServiceLocator approach to register all my C# classes and make the accessible.
the monobehaviour classes are esolving their dependencies on Start, which is not ideal but works well in a small project like this.

Character- i generated the chatacter with chatGPT and made the running animation