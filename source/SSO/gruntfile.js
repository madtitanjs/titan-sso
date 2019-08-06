/// <binding ProjectOpened='sass, watch' />
const sass = require('node-sass')
module.exports = function (grunt) {
    'use strict';

    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        // Clean
        clean: ["wwwroot/css/*", 'site.scss'],
        // Sass
        sass: {
            options: {
                sourceMap: true, // Create source map
                outputStyle: 'compressed', // Minify output
                implementation: sass
            },
            dist: {
                files: [
                    {
                        expand: true, // Recursive
                        cwd: "./", // The startup directory
                        src: ["site.scss"], // Source files
                        dest: "wwwroot/css", // Destination
                        ext: '.css'
                    }
                ]
            }
        },
        concat: {
            all: {
                src: ['Styles/**/*.scss'],
                dest: 'site.scss'
            }
        },
        watch: {
            tasks: ["all"],
            files: ["Styles/**/*.scss"]
        }
    });

    // Load the plugin
    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-watch');
    // Default task(s).
    grunt.registerTask('default', ['sass']);
    grunt.registerTask('all', ['clean','concat','sass'])
};