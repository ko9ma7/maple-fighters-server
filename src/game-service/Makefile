publish:
	dotnet publish --runtime alpine-x64 -c Release --self-contained true -o ./publish /p:PublishSingleFile=true /p:PublishTrimmed=true

build:
	docker build -t game-service:1.0.0 .

run:
	docker run -p 50051:50051 game-service:1.0.0