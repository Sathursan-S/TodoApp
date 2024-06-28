pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Checkout the source code from your version control system
                checkout scm
            }
        }

        //         stage('Build Frontend') {
        //     steps {
        //         script {
        //             // Navigate to the frontend directory and build the Docker image
        //             dir('frontend/todo-app') {
        //                 bat 'docker build -t todo-frontend .'
        //             }
        //         }
        //     }
        // }

        // stage('Build Backend') {
        //     steps {
        //         script {
        //             // Navigate to the backend directory and build the Docker image
        //             dir('backend/TodoApi') {
        //                 bat 'docker build -t todo-backend .'
        //             }
        //         }
        //     }
        // }

        stage('Deploy') {
            steps {
                script {
                    // Deploy the Docker Compose application
                   
                    bat 'docker-compose up --build -d'
                }
            }
        }
    }

    post {

        success {
            // Notify on successful build
            echo 'Build and deployment successful!'
        }
        failure {
            // Notify on failed build
            echo 'Build or deployment failed!'
        }
    }
}
