pipeline {
    agent { docker { image 'mcr.microsoft.com/dotnet/sdk:6.0' } }
    stages {
        stage('Checkout') {
            steps {
                git(url: 'https://github.com/nkalion/listic', branch: 'master')
            }
        }
        stage('Restore packages') {
            steps {
                sh 'dotnet restore Listic.sln -v diag'
            }
        }
        stage('Clean') {
            steps {
                sh 'dotnet clean Listic.sln --configuration Release'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build Listic.sln --configuration Release --no-restore'
            }
        }
    }
}