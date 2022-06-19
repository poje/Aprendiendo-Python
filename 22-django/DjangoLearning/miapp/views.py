from django.shortcuts import render, HttpResponse, redirect

# Create your views here.
# MVC = Modelo Vista Controlador => Acciones (Metodos)
# MVT = Modelo Template Vista => Acciones (Metodos)


layout = """ 

<h1> Sitio Web con Django - Jorge Villaseca R. </h1>
<hr />
<ul>
    <li>
        <a href="/inicio">Inicio</a>
    </li>
    <li>
        <a href="/holamundo">Hola Mundo !</a>
    </li>
    <li>
        <a href="/pagina">pagina</a>
    </li>
    <li>
        <a href="/contacto">contacto</a>
    </li>
</ul>
<hr />

"""


def index(request):
    
    html = """
        <h1>Inicio</h1>
        <p>Años hasta el 2050:</p>
        <ul>
    """
    year = 2021
    while year <= 2050:

        if year % 2 == 0:
            html += f"<li> { str(year) } </li>"
        
        year += 1

    html += "</ul>"

    return render(request, 'index.html')

def hola_mundo(request):
    return render(request, 'hola_mundo.html')

def pagina(request, redirigir=0):

    if redirigir == 1:
        # return redirect('/inicio')
        return redirect('contacto', nombre="poje")

    return render(request, 'pagina.html')

def contacto(request, nombre="Jorge"):
    return HttpResponse(layout+ f"""<h2>Contacto {nombre} </h2>""")