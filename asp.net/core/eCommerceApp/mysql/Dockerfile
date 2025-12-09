# Base image for the build stage
FROM alpine:latest AS builder

# Create a directory (in container) to hold initialization scripts
WORKDIR /app

# Copy initialization scripts from the host to the container
COPY mysql-init /app

# Install required packages (adjust based on your script needs)
RUN apk add --no-cache openssl

# Base image for the final MySQL image
FROM mysql:latest

# Copy initialization scripts from the build stage
COPY --from=builder /app /docker-entrypoint-initdb.d

# Set the MySQL environment variables
ENV MYSQL_ALLOW_EMPTY_PASSWORD=yes

# Expose the MySQL port
EXPOSE 3306