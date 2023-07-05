# aws-queue-and-topic

Run this command to install Lambda Tools: dotnet tool install -g Amazon.Lambda.Tools

Then it should be able to build packages for deployment: dotnet lambda package --configuration release --framework net6.0 --output-package ./bin/Release/net6.0/publish/deploy-package.zip

Then create the bucket to upload the package for the lambda:  aws s3api create-bucket --acl private --bucket dev-any-source-code --region sa-east-1 --create-bucket-configuration LocationConstraint=sa-east-1

Then put the app package into the bucket: aws s3api put-object --bucket dev-any-source-code --key aws-queue-and-topic/deploy-package.zip --body ./bin/Release/net6.0/publish/deploy-package.zip

Then publish cloud formation template: aws cloudformation deploy --template-file CloudFormationTemplate.json --stack-name test-stack --capabilities CAPABILITY_NAMED_IAM