
describe('Test Login 1', () => {
    it('No debe pasar Login ok', () => {

        cy.visit('https://localhost:44320/Auth/Login')
        cy.get(':nth-child(1) > .form-control').type('admin3');
        cy.get(':nth-child(2) > .form-control').type('admin4');
        cy.get('.btn').click();
        cy.get('.col-md-4 > p').should('have.text', 'Usuario y/o contraseña incorrecta');
    })
})

describe('Test Login 2', () => {
    it('Debe pasar Login ok', () => {

        cy.visit('https://localhost:44320/Auth/Login')
        cy.get(':nth-child(1) > .form-control').type('admin');
        cy.get(':nth-child(2) > .form-control').type('admin');
        cy.get('.btn').click();
        cy.get('h2').should('have.text', 'Libros');
    })
})

describe('Biblioteca 1', () => {
    it('Debe cargar Mi Biblioteca y regresar al home', () => {

        cy.visit('https://localhost:44320/Auth/Login')
        cy.get(':nth-child(1) > .form-control').type('admin');
        cy.get(':nth-child(2) > .form-control').type('admin');
        cy.get('.btn').click();
        cy.get('h2').should('have.text', 'Libros');
        cy.get(':nth-child(2) > .nav-link').click();
        cy.get('h2').should('have.text', 'Mi Biblioteca');
        cy.get(':nth-child(1) > .nav-link').click();
        cy.get('h2').should('have.text', 'Libros');
    })
})

describe('Biblioteca 2', () => {
    it('Debe cargar home y agregar libro a biblioteca', () => {

        cy.visit('https://localhost:44320/Auth/Login')
        cy.get(':nth-child(1) > .form-control').type('admin');
        cy.get(':nth-child(2) > .form-control').type('admin');
        cy.get('.btn').click();
        cy.get('h2').should('have.text', 'Libros');
        cy.get(':nth-child(1) > .thumb-wrapper > .thumb-content > .btn').click();
        cy.get('.alert').should('include.text', 'Se añádio el libro a su biblioteca');
    })
})

describe('Biblioteca 1', () => {
    it('Debe cargar Mi Biblioteca marcar un libro como Leyendo', () => {

        cy.visit('https://localhost:44320/Auth/Login')
        cy.get(':nth-child(1) > .form-control').type('admin');
        cy.get(':nth-child(2) > .form-control').type('admin');
        cy.get('.btn').click();
        cy.get('h2').should('have.text', 'Libros');
        cy.get(':nth-child(2) > .nav-link').click();
        cy.get('h2').should('have.text', 'Mi Biblioteca');
        cy.get(':nth-child(3) > :nth-child(4) > a').click();
        cy.get(':nth-child(1) > :nth-child(4) > a').should('have.text', 'Terminado');
    })
})


describe('Biblioteca 1', () => {
    it('Debe cargar Mi Biblioteca marcar un libro como Terminado', () => {

        cy.visit('https://localhost:44320/Auth/Login')
        cy.get(':nth-child(1) > .form-control').type('admin');
        cy.get(':nth-child(2) > .form-control').type('admin');
        cy.get('.btn').click();
        cy.get('h2').should('have.text', 'Libros');
        cy.get(':nth-child(2) > .nav-link').click();
        cy.get('h2').should('have.text', 'Mi Biblioteca');
        cy.get(':nth-child(1) > :nth-child(4) > a').click();
        cy.get(':nth-child(1) > :nth-child(3) > span').should('have.text', 'TERMINADO');
    })
})