module.exports = function(grunt) 
{
	grunt.loadNpmTasks('grunt-dotnet-assembly-info');
    grunt.registerTask('default', []);
    grunt.registerTask('ci', ['assemblyinfo']);

    grunt.initConfig(
    {
    	assemblyinfo: 
    	{
            options: 
            {
                files: ['src/AutoGrid.sln'],
                info: 
                {
                    title: 'Space Squirrel AutoGrid',
		            description: 'Magical replacement for the default WPF Grid.', 
		            configuration: 'Release', 
		            company: 'Space Squirrel', 
		            product: 'Space Squirrel AutoGrid', 
		            copyright: 'Copyright 3002 © Space Squirrel', 
		            trademark: 'Space Squirrel', 
		            version: '0.1.0',
		            fileVersion: '0.1.0'
                }
            }
        }
    });
}
