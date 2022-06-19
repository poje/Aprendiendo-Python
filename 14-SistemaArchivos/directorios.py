import os
import shutil

# Crear carpeta
if not os.path.isdir("./mi_carpeta"):
    os.mkdir("./mi_carpeta")
else:
    print("ya existe el directorio")

# Eliminar Carpeta
# os.rmdir("./mi_carpeta")

# Copiar
# ruta_original = "./mi_carpeta"
# ruta_nueva = "./mi_carpeta_copiada"


# shutil.copytree(ruta_original, ruta_nueva)

# Eliminar
# os.rmdir(ruta_nueva)

print("Contenido de mi carpeta")
contenido = os.listdir("./mi_carpeta")
print(contenido)
