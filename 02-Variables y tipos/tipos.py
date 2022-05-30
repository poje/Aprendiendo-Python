nada = None
cadena = "Hola"
entero = 99
flotante = 4.2
booleano = True
lista = [10,20,30,40,50]
tuplaNoCambia = ("master", "en", "python")
diccionario = {
    "nombre": "Jorge",
    "Apellido": "Villaseca",
    "curso": "Master en python"
}
rango = range(9)
dato_byte = b"probando"

#Imprimir variable
#print(nada)
#print(cadena)
#print(entero)
#print(flotante)
#print(booleano)
#print(lista)
#print(tuplaNoCambia)
#print(diccionario)
#print(rango)

print(dato_byte)


#Mostrar tipo de datos
#print(type(nada))
#print(type(cadena))
#print(type(entero))
#print(type(flotante))
#print(type(booleano))
#print(type(lista))
#print(type(tuplaNoCambia))
#print(type(diccionario))
#print(type(rango))

print(type(dato_byte))

#Conversion de un tipo de dato a otro

texto = "Hola soy un texto"
numerito = str(7759)
print(texto + " " + numerito)

numerito = int(775)
numerito = float(775) # 775.0