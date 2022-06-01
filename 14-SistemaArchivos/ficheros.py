from io import open

import pathlib

# Abrir Archivos

# Abrir Archivo
ruta = str(pathlib.Path().absolute()) + "/fichero_texto.txt"
print(ruta)

archivo = open(ruta, "a+")

# Escribir archivo

archivo.write("************ Soy un Texto from python **************\n")


# cerrar archivos
archivo.close()


# Abrir archivo para leer

ruta = str(pathlib.Path().absolute()) + "/fichero_texto.txt"
archivo_lectura = open(ruta, "r")

# Leer contenido

# contenido = archivo_lectura.read()

# print(contenido)

# Leer contenido y guardar en lista

lista = archivo_lectura.readlines()
archivo_lectura.close()

for frase in lista:
    lista_frase = frase.split()
    print(lista_frase)

# print(lista)