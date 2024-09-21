FROM mysql:8.0

ENV MYSQL_ROOT_PASSWORD=1234
ENV MYSQL_DATABASE=TASKDB

COPY ./Infra/Docker/mysql/backup.sql /docker-entrypoint-initdb.d/

EXPOSE 3306
