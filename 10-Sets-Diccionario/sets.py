"""

SET es un tipo de datos para tener una coleccion de valores, pero no tiene indice ni orden

"""


persona = {
    "Jorge",
    "Manuel",
    "Francisco"
}

persona.add("Andres")
persona.remove("Manuel")

print(type(persona))
print(persona)