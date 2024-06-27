pipeline {
   agent any

    stages {
        stage('Checkout') {
            steps {
                // Checkout the source code from your version control system
                checkout scm
            }
        }

        stage('Build Backend') {
            steps {
                script {
                    // Navigate to the backend directory and build the Docker image
                    dir('backend/TodoApi') {
                        sh 'docker build -t todo-backend .'
                    }
                }
            }
        }

        stage('Build Frontend') {
            steps {
                script {
                    // Navigate to the frontend directory and build the Docker image
                    dir('frontend/todo-app') {
                        sh 'docker build -t todo-frontend .'
                    }
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    // Deploy the Docker Compose application
                    sh 'docker-compose down'
                    sh 'docker-compose up -d'
                }
            }
        }
    }

    post {
        always {
            // Clean up Docker resources
            sh 'docker system prune -f'
        }
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
