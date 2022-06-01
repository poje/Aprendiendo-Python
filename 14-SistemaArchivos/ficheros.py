from io import open

import pathlib
import shutil
import os

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

# Copiar
ruta_original = str(pathlib.Path().absolute()) + "//fichero_texto.txt"
ruta_nueva = str(pathlib.Path().absolute()) + "//fichero_texto_copiado.txt"

shutil.copyfile(ruta_original, ruta_nueva)

# Mover
ruta_original = str(pathlib.Path().absolute()) + "//fichero_texto_copiado.txt"
ruta_nueva_mov = str(pathlib.Path().absolute()) + "//fichero_texto_copiado_mov.txt"

shutil.move(ruta_original, ruta_nueva_mov)

# Eliminar archivos

os.remove(ruta_nueva_mov)   

# print(os.path.abspath("./"))

ruta_comprobar = "fichero_texto.txt"
if os.path.isfile(ruta_comprobar):
    print("El archivo existe")
else:
    print("El archivo no existe")