pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        git(url: 'https://github.com/nkalion/listic', branch: 'master')
      }
    }

    stage('DotNet build') {
      steps {
        withDotNet(sdk: 'dotnet 6') {
          sh 'dotnet build Listic.sln -c Release'
        }
      }
    }

  }
}