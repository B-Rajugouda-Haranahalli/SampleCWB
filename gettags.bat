
for /F "eol=; tokens=2,3* delims=," %i in ('docker images ^2 >') do @echo %i %j %k