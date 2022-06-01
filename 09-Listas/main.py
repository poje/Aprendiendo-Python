from ast import For
import email


pelicula = "Batman"
peliculas = ["Batman", "Spiderman", "El señor de los anillos"]

cantantes = list(("2pac", "Drake", "Linkin Park"))

years = list(range(2020, 2050))

variada = ["Jorge", 31, True]

print(peliculas)

print(cantantes)

print(years)

print(variada)

# Indices
print("### indices ###")
print(peliculas[1])
print(peliculas[-2])
print(cantantes[2:3])
print(peliculas[2:])


# Añadir elementos a listas
cantantes.append("Eminem")
print(cantantes)

# Recorrer Lista
# print("\n##### listado de peliculas #####")

# nueva_pelicula = ""

# while nueva_pelicula != "parar":
#     nueva_pelicula = input("Introduce la nueva pelicula: ")
#     if nueva_pelicula != "parar":
#         peliculas.append(nueva_pelicula)


# for pelicula in peliculas:
#     print(f"{peliculas.index(pelicula)+1} - {pelicula} ")

# Listas multidimensionales

print("\n##### Lista de contactos #####")

contactos = [
    [
        'Antonio',
        'antonio@antonio.cl'
    ],
    [
        'jorge',
        'jorge@jorge.cl'
    ],
    [
        'pablo',
        'pablo@pablo.cl'
    ]
]

print(contactos)

for contacto in contactos:
    for elemento in contacto:
        if contacto.index(elemento) == 0:
            print("Nombre: " + elemento)
        else:
            print("Email: " + elemento)
    print("\n")

