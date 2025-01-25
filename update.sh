#!/usr/bin/env bash

IFS='
'
export $(grep -E -v '^#' .env | xargs -0)
IFS=
prodDir=$PWD
branch=$@
if [  -z "$@" ]
  then
    if [  -z "$PROJECTS_BRANCH" ]
      then
        branch=release
      else
        branch=$PROJECTS_BRANCH
      fi
fi

echo "Working branch " $branch

git fetch
git reset --hard origin/$branch

cd $prodDir && docker-compose up -d --build