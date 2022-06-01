"""
Modulos: son funcionalidades ya hechas para que puedan ser reutilizadas

"""

# Importar modulo propio para poder usar las funciones dentro del módulo
# import mimodulo


# from mimodulo import holamundo

# print(mimodulo.holamundo("Jorge"))

# print(mimodulo.calculadora(5,5, True))

# print(holamundo("jorge"))

# from mimodulo import *

# print(holamundo("jorge"))
# print(calculadora(5,2,True))


# Modulo Fechas

import datetime

print(datetime.date.today())

fecha_completa = datetime.datetime.now()

print(fecha_completa)
print(fecha_completa.year)
print(fecha_completa.month)
print(fecha_completa.day)

fecha_personalizada = fecha_completa.strftime("%d/%m/%Y, %H:%M:%S")
print(fecha_personalizada)

# Modulo Matematicas

import math

print("Raiz cuadrada de 10 ", math.sqrt(10))

print("Numero de PI", float(math.pi))

print("redondear ", math.ceil(6.56789))

print("redondear ", math.floor(6.56789))

# Modulo Random

import random

print("Numero aleatorio entre 15 y 67", random.randint(15,67))
