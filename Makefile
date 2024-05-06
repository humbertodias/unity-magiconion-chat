build:
	dotnet build
	make rm-obj

rm-obj:
	find . -type d \( -name "obj" \) -depth -execdir rm -rf {} \;
	
rm-bin:
	find . -type d \( -name "bin" \) -depth -execdir rm -rf {} \;

clean:
	dotnet clean
	make rm-obj rm-bin

docker:
	DOCKER_DEFAULT_PLATFORM=linux/amd64 \
	docker build -t chat_magiconion:latest -f Chat.Server/Dockerfile .