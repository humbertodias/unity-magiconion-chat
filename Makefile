build:
	dotnet build

clean:
	dotnet clean
	find . -type d \( -name "bin" -o -name "obj" \) -depth -execdir rm -rf {} \;

docker:
	DOCKER_DEFAULT_PLATFORM=linux/amd64 \
	docker build -t chat_magiconion:latest -f Chat.Server/Dockerfile .