bandas = ["myrath", "kamelot", "galneryus", "rhapsody"]

numeros = [1,2,5,8,3]

print(numeros)

# Ordenar
numeros.sort()
print(numeros)

# Agregar elementos
bandas.append("iron maiden")
# print(bandas)

bandas.insert(1, "Slayer")
# print(bandas)

# Eliminar Elementos
bandas.pop(1)
# print(bandas)

bandas.remove("kamelot")
print(bandas)

# Dar vuelta la lista
print(numeros)
numeros.reverse()
print(numeros)

# Buscar dentro de la lista
print('rhapsody' in bandas)

# Contar elementos
print(len(bandas))

# Cuantas veces aparece un elemento
numeros.append(8)
print(numeros.count(8))

# Conseguir Indice
print(bandas.index("galneryus"))

# Unir listas
bandas.extend(numeros)
print(bandas)