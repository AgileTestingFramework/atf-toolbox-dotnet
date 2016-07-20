
properties {
  $SolutionFile = "atf-toolbox.sln"
  $ProjectFile = "atf.toolbox.csproj"
}


# Only Cleans
Task CleanTask  {
    $CleanList = @(
        'bin'
        'obj'
        'packages'
    )
    foreach($Item in $CleanList) {
        try{
            Get-ChildItem $Item -Directory -Recurse | Remove-Item -Force -Confirm:$false -Recurse -ErrorAction SilentlyContinue
        } Catch {
            # Ignore any error
        }
    }
    Remove-Item .\*.nupkg -Force -Confirm:$false -ErrorAction SilentlyContinue
}

# Only NuGet restores
Task NuGetRestoreTask {
    Exec {nuget restore $SolutionFile  }
}

# Only MSBuild compiles 
Task CompileTask {
    Exec {msbuild $SolutionFile  "/t:Clean;Rebuild" /m }
}

# Only NuGet packs
Task PackTask {
    Exec {nuget pack "atf.toolbox.nuspec" }
}
# Only NuGet publishes
Task PublishTask {
    
    $NuGetApiKey = Get-Content .\nuget-api-key.txt
    if($NuGetApiKey){
        Exec {nuget push "*.nupkg" -ApiKey $NuGetApiKey -Source https://www.nuget.org/api/v2/package }
    } else {
        Write-Output "Define NuGet API key"
    }
    
}

# 
Task Default -Depends Clean, Compile

Task Compile -Depends Clean, NuGetRestoreTask, CompileTask  

Task Pack -Depends Compile, PackTask

Task Publish -Depends Pack, PublishTask

Task Clean -Depends CleanTask 

 