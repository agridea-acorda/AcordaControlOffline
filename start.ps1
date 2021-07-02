docker build . -t acapp
docker run -d -p 5000:80 acapp