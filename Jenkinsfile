pipeline {
  agent any
  stages {
    stage('DotNet build') {
      steps {
        withDotNet() {
          sh 'dotnet build Listic.sln -c Release'
        }

      }
    }

    stage('Checkout') {
      steps {
        git(url: 'https://github.com/nkalion/listic', branch: 'master')
      }
    }

  }
}