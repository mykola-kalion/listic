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

  }
}