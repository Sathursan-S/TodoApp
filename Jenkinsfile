#!groovy
pipeline {
    agent any
    
    stages {
        stage('Docker compose') {
            steps {
                script {
                    sh 'docker compose up'
                }
            }
        }
    }
}