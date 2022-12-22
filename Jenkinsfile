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
        withDotNet(sdk: '6.0.404') {
          sh 'dotnet build Listic.sln -c Release --sdk-version 6.0.404'
        }

      }
    }

  }
}