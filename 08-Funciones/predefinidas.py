nombre = "Jorge"

# Predifinidas

print(type(nombre))

# detectar el tipado
comprobar = isinstance(nombre, str)

if comprobar:
    print("esa variable es un string")
else:
    print("la variable no es string")

if not isinstance(nombre, float):
    print("la variable no es un numero")

# Limpiar espacios 

frase = "    mi contenido   "
print(frase)
print(frase.strip())

# Eliminar variables
year = 2022
print(year)
del year
# print(year)


# Comprobar variable vacia

texto = "   ff    "

if len(texto) <= 0:
    print("la variable esta vacia")
else:
    print("la variable tiene contenido ", len(texto))


# Encontrar caracteres+

frase = "la vida es bella"

print(frase.find("vida"))

# Reemplazar palabras

nueva_frase = frase.replace("vida", "moto")

print(nueva_frase)

# Mayusculas y minusculas

print(nombre)
print(nombre.capitalize())
print(nombre.lower())
print(nombre.upper())