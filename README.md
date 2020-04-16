# RateLimiter-Perimeterx
How to run:
```
1.Go to project folder
2.cd RateLimiterAPI
3.Build the docker:
  docker build -t ratelimiter .
4.Run the docker with the requested environment variables
  docker run -d -p 8080:80 -e ttl=TTL -e threshold=THRESHOLD --name limiterContainer ratelimiter
```
