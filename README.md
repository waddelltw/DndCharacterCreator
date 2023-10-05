# DndCharacterCreator

This project is a web app to create DnD characters and access them using accounts!

This app is made with ASP.NET, using the MVC design pattern and uses a SQLite Database with Entity Framework.

To run locally, you can either run in visual studio or run in Docker.

To run in visual studio, simply clone this repo, and open the DndCharacterCreator.sln file with visual studio. You can then press the play button on visual studio to start the server.

To run in Docker, you need to build the docker image and then run the image in a container.
1. In the root folder, run `docker build -f ./DndCharacterCreator/Dockerfile --force-rm -t dndcharacter .`
2. Then run `docker run -d -p 8080:80 --name DndCharacterCreatorLocal dndcharacter`
3. You can then go to `localhost:8080` in your browser to see the application
