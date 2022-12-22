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
          sh 'ls -la'
        }

      }
    }

    stage('Another build') {
      steps {
        dotnetBuild(showSdkInfo: true, sdk: 'dotnet 6', configuration: 'Release', continueOnError: true)
      }
    }

  }
}