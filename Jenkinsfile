import groovy.json.JsonSlurper

VERSION_NUMBER = ""

/** Pipeline **/
node {
//     ws('netcore') {
        try{
            stage("scm pull") {
		deleteDir();
		cloneRepo();
//                 VERSION_NUMBER = getVersionNumber();
                currentBuild.displayName = "$VERSION_NUMBER";
            }

            stage ("dotnet build") {
		dotnetBuild();
            }

//             stage ("dotnet test") {
// 		dotnet_test();
//             }
//
//             stage ("dotnet publish") {
// 		dotnet_publish();
//             }
//
//             stage ("docker build") {
// 		docker_build();
//             }
//
//             stage ("docker run") {
// 		docker_run();
//             }
        }
        catch (InterruptedException x) {
            currentBuild.result = 'ABORTED';
            throw x;
        }
        catch (e) {
            currentBuild.result = 'FAILURE';
            throw e;
        }
//     }
}

def dotnet_build(){
    dir('Listic') {
	sh(script: 'dotnet build Listic.sln -c Release', returnStdout: true);
    }
}

def cloneRepo() {
    checkout scm;
}
