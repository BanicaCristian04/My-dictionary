# My-dictionary


Aceast proiect este o aplicatie WPF construita cu C# in .NET Framework care consta in trei module principale: Administrativ, Cautare de cuvinte si Divertisment.
# Functionare
<b>1.Modulul administrativ</b>
<ul>
<li>
 <b></b>Autentificare utilizator</b>: Pentru accesarea acestui modul, se solicita numele de utilizator si parola. Conturile de utilizator sunt stocate intr-un fisier text (users.txt) 
</li>
<li>
  <b>Operatii CRUD</b>: Permite adaugarea, modificarea și stergerea cuvintelor din dictionar. Fiecare cuvant are o descriere, o categorie si o imagine optionala.
</li>
<li>Gestionare imagini: Imaginile se pot incarca de pe calculatorul utilizatorului si se stocheaza in directorul Resources/Images/. Daca un cuvant nu are o imagine asociata, se foloseste o imagine implicita.
</li>  
</ul>

<b>2.Modulul de cautare de cuvinte</b>
<ul>
  <li>
  <b>Cautare dupa categorie sau cuvant cheie</b>: Utilizatorii pot selecta o categorie sau pot cauta direct cuvinte.
</li>
<li>
  <b>Autocompletare</b>: Textbox-ul ofera sugestii de cuvinte pe masura ce utilizatorul tasteaza.
</li>
<li>
  <b>Afisare detalii</b>: La selectarea unui cuvant, se afiseaza cuvantul, descrierea, categoria si imaginea asociata.
</li>
</ul>

<b>3. Modulul divertisment</b>
<ul>
  <li>
  <b>Joc de ghicire a cuvintelor</b>: Utilizatorii trebuie sa ghiceasca cuvinte pe baza descrierilor sau imaginilor oferite. ( cuvintele sunt alese aleatoriu pentru fiecare joc)
  La finalul jocului, se afiseaza scorul jucatorului, iar acesta poate sa inceapa o noua runda.
</li>
</ul>

