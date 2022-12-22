import groovy.json.JsonSlurper

VERSION_NUMBER = ""

/** Pipeline **/
node {
    try {
        stage("scm pull") {
            deleteDir();
            cloneRepo();
            currentBuild.displayName = "$VERSION_NUMBER";
        }

        stage("dotnet build") {
            project("Listic") {
                dotnetBuild()
            }
        }

    }
    catch (InterruptedException x) {
        currentBuild.result = 'ABORTED';
        throw x;
    }
    catch (e) {
        currentBuild.result = 'FAILURE';
        throw e;
    }
}

def cloneRepo() {
    checkout scm;
}
