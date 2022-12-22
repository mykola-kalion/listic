pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        git(url: 'https://github.com/nkalion/listic', branch: 'master')
      }
    }

//         stage('Build'){
//             steps{
//                 sh 'dotnet restore Listic.sln'
//             }
//         }
//         stage('Clean'){
//             steps{
//                 sh 'dotnet clean Listic.sln --configuration Release'
//             }
//         }
        stage('Build'){
            steps{
                sh 'dotnet build Listic.sln --configuration Release --no-restore'
            }
        }

  }
}