FROM alpine:latest

# Add some libs required by .NET runtime 
RUN apk add --no-cache libstdc++ libintl

# Copy 
WORKDIR /app
COPY ./publish ./

# See: https://github.com/dotnet/announcements/issues/20
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT true
# RUN apk add --no-cache icu-libs
# ENV LC_ALL en_US.UTF-8
# ENV LANG en_US.UTF-8

ENTRYPOINT ["./Game.AppStarter"]