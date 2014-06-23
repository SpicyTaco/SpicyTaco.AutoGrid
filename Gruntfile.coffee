module.exports = (grunt) -> 
  require('time-grunt')(grunt)

  grunt.initConfig
    assemblyinfo:
      options:
        files: ['src/AutoGrid.sln']
        info:
          title: 'Space Squirrel AutoGrid'
          description: 'Magical replacement for the default WPF Grid.'
          configuration: 'Debug'
          company: 'Space Squirrel'
          product: 'Space Squirrel AutoGrid'
          copyright: 'Copyright 3002 © Space Squirrel'
          trademark: 'Space Squirrel'
          version: '0.1.0'
          fileVersion: '0.1.0'
    nugetrestore:
      restore:
        src: 'src/AutoGrid.sln'
        dest: 'src/packages/'
    msbuild:
      src: ['src/AutoGrid.sln']
      options:
        projectConfiguration: 'Debug'
        targets: ['Clean', 'Rebuild']
        stdout: true
        version: 4.0
        verbosity: 'quiet'
    mspec:
      options:
        toolsPath: 'src/packages/Machine.Specifications.0.8.1/tools'
        output: 'reports/mspec'
      specs:
        src: ['src/**/bin/Debug/*.Tests.dll']

  grunt.task.registerTask 'banner', () -> 
    console.log(grunt.file.read('banner.txt'))

  grunt.loadNpmTasks 'grunt-dotnet-assembly-info'
  grunt.loadNpmTasks 'grunt-msbuild'
  grunt.loadNpmTasks 'grunt-nuget'
  grunt.loadNpmTasks 'grunt-dotnet-mspec'

  grunt.registerTask 'default', ['ci']
  grunt.registerTask 'ci', ['banner','assemblyinfo', 'nugetrestore', 'msbuild', 'mspec']

  null
