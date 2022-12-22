pipeline {
  agent any
  stages {
    stage('DotNet build') {
      steps {
        dotnetBuild(targets: 'Listic.sln')
      }
    }

  }
}