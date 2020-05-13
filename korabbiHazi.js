const BASE_URL = 'https://jsonplaceholder.typicode.com';

let usersDivEl;
let postsDivEl;
let loadButtonEl;
let albumsDivEl;


function createPostsList(posts) {
    const ulEl = document.createElement('ul');

    for (let i = 0; i < posts.length; i++) {
        const post = posts[i];

        // creating paragraph
        const dataPostIdAttr = document.createAttribute('data-post-id');
        dataPostIdAttr.value = post.id;
        const strongEl = document.createElement('strong');
        const buttonEl = document.createElement('button');
        buttonEl.addEventListener('click', onLoadComments);
        buttonEl.setAttributeNode(dataPostIdAttr);
        buttonEl.textContent = post.title;

        const pEl = document.createElement('p');
        pEl.setAttribute('id', 'post-paraghraph' + post.id);
        pEl.setAttribute('class', 'posts');
        
        pEl.appendChild(buttonEl);
        pEl.appendChild(strongEl); 
        
        pEl.appendChild(document.createTextNode(`${post.body}`));

        // creating list item
        const liEl = document.createElement('li');
        liEl.appendChild(pEl);

        ulEl.appendChild(liEl);
    }

    return ulEl;
}

function createCommentsList(comments) {
    const ulEl = document.createElement('ul');

    for (let i = 0; i < comments.length; i++) {
        const comment = comments[i];

        // creating paragraph
        const strongEl = document.createElement('strong');
        strongEl.textContent = comment.body;
        const pEl = document.createElement('p');

        pEl.appendChild(strongEl); 

        const liEl = document.createElement('li');
        liEl.appendChild(pEl);

        ulEl.appendChild(liEl);
    }

    return ulEl;
}

function onPostsReceived() {
    postsDivEl.style.display = 'block';
    albumsDivEl.style.display = 'none';

    const text = this.responseText;
    const posts = JSON.parse(text);

    const divEl = document.getElementById('posts-content');
    while (divEl.firstChild) {
        divEl.removeChild(divEl.firstChild);
    }
    divEl.appendChild(createPostsList(posts));
}

function onLoadPosts() {
    const el = this;
    const userId = el.getAttribute('data-user-id');

    const xhr = new XMLHttpRequest();
    xhr.addEventListener('load', onPostsReceived);
    xhr.open('GET', BASE_URL + '/posts?userId=' + userId);
    xhr.send();
}

function createUsersTableHeader() {
    const idTdEl = document.createElement('td');
    idTdEl.textContent = 'Id';

    const nameTdEl = document.createElement('td');
    nameTdEl.textContent = 'Name';

    const trEl = document.createElement('tr');
    trEl.appendChild(idTdEl);
    trEl.appendChild(nameTdEl);

    const theadEl = document.createElement('thead');
    theadEl.appendChild(trEl);
    return theadEl;
}

function createUsersTableBody(users) {
    const tbodyEl = document.createElement('tbody');

    for (let i = 0; i < users.length; i++) {
        const user = users[i];

        // creating id cell
        const idTdEl = document.createElement('td');
        idTdEl.textContent = user.id;

        // creating name cell
        const dataUserIdAttr = document.createAttribute('data-user-id');
        dataUserIdAttr.value = user.id;

        const buttonEl = document.createElement('button');
        buttonEl.textContent = user.name;
        buttonEl.setAttributeNode(dataUserIdAttr);
        buttonEl.addEventListener('click', onLoadPosts);

        const nameTdEl = document.createElement('td');
        nameTdEl.appendChild(buttonEl);

        //create album button
        const albumUserIdAttr =  document.createAttribute('album-user-id');
        albumUserIdAttr.value = user.id;
        
        const albumButton = document.createElement('button');
        albumButton.textContent = 'Albums';
        albumButton.setAttributeNode(albumUserIdAttr);
        albumButton.addEventListener('click', onLoadAlbums);

        const albumTdEl = document.createElement('td');
        albumTdEl.appendChild(albumButton);


        // creating row
        const trEl = document.createElement('tr');
        trEl.appendChild(idTdEl);
        trEl.appendChild(nameTdEl);
        trEl.appendChild(albumTdEl);

        tbodyEl.appendChild(trEl);
    }

    return tbodyEl;
}

function createUsersTable(users) {
    const tableEl = document.createElement('table');
    tableEl.appendChild(createUsersTableHeader());
    tableEl.appendChild(createUsersTableBody(users));
    return tableEl;
}

function onUsersReceived() {
    loadButtonEl.remove();

    const text = this.responseText;
    const users = JSON.parse(text);

    const divEl = document.getElementById('users-content');
    divEl.appendChild(createUsersTable(users));
}

function onLoadUsers() {
    const xhr = new XMLHttpRequest();
    xhr.addEventListener('load', onUsersReceived);
    xhr.open('GET', BASE_URL + '/users');
    xhr.send();
}

function onCommentsReceived() {
    const text = this.responseText;
    const comments = JSON.parse(text);
    const postId = comments[0].postId;
    const divEl = document.getElementById('post-paraghraph'+postId);
    const postsDiv = document.getElementsByClassName('posts');
    
    Array.from(postsDiv).forEach(element => {
        while(element.childNodes.length > 3){
            element.removeChild(element.lastChild);
        }
    });

    divEl.appendChild(createCommentsList(comments));
    
}

function onLoadComments() {
    const el = this;
    const postId = el.getAttribute('data-post-id');

    const xhr = new XMLHttpRequest();
    xhr.addEventListener('load', onCommentsReceived);
    xhr.open('GET', BASE_URL + '/comments?postId=' + postId);
    xhr.send();
}

function createAlbumsList(albums) {
    const ulEl = document.createElement('ul');

    for (let i = 0; i < albums.length; i++) {
        const album = albums[i];

        // creating paragraph
        const dataAlbumIdAttr = document.createAttribute('data-album-id');
        dataAlbumIdAttr.value = album.id;
        const buttonEl = document.createElement('button');
        buttonEl.addEventListener('click', onLoadPhotos);
        buttonEl.setAttributeNode(dataAlbumIdAttr);
        buttonEl.textContent = album.title;

        const pEl = document.createElement('p');
        pEl.setAttribute('id', 'album-paraghraph' + album.id);
        pEl.setAttribute('class', 'albums');
        
        pEl.appendChild(buttonEl);

        

        // creating list item
        const liEl = document.createElement('li');
        liEl.appendChild(pEl);

        ulEl.appendChild(liEl);
    }

    return ulEl;
}

function onAlbumsReceived() {
    albumsDivEl.style.display = 'block';
    postsDivEl.style.display = 'none';
    const text = this.responseText;
    const albums = JSON.parse(text);

    const divEl = document.getElementById('albums-content');
    while (divEl.firstChild) {
        divEl.removeChild(divEl.firstChild);
    }
    divEl.appendChild(createAlbumsList(albums));
}

function onLoadAlbums() {
    const el = this;
    const albumId = el.getAttribute('album-user-id');

    const xhr = new XMLHttpRequest();
    xhr.addEventListener('load', onAlbumsReceived);
    xhr.open('GET', BASE_URL + '/albums?userId=' + albumId);
    xhr.send();
}

function createPhotosList(photos) {
    const ulEl = document.createElement('ul');

    for (let i = 0; i < photos.length; i++) {
        const photo = photos[i];

        // creating paragraph
        const imgEl = document.createElement("img");
        imgEl.src = photo.url;
        imgEl.width = 300;
        imgEl.height = 300;
        const strongEl = document.createElement('strong');
        strongEl.textContent = photo.url;
        const pEl = document.createElement('p');

        pEl.appendChild(imgEl); 

        const liEl = document.createElement('li');
        liEl.appendChild(pEl);

        ulEl.appendChild(liEl);
    }

    return ulEl;
}

function onPhotosReceived(){
    const text = this.responseText;
    const photos = JSON.parse(text);
    const albumId = photos[0].albumId;
    const divEl = document.getElementById('album-paraghraph'+albumId);
   const albumsDiv = document.getElementsByClassName('albums');
    
    Array.from(albumsDiv).forEach(element => {
        while(element.childNodes.length > 1){
            element.removeChild(element.lastChild);
        }
    });

    divEl.appendChild(createPhotosList(photos));
}

function onLoadPhotos() {
    const el = this;
    const albumId = el.getAttribute('data-album-id');

    const xhr = new XMLHttpRequest();
    xhr.addEventListener('load', onPhotosReceived);
    xhr.open('GET', BASE_URL + '/photos?albumId=' + albumId);
    xhr.send();
}

document.addEventListener('DOMContentLoaded', (event) => {
    usersDivEl = document.getElementById('users');
    postsDivEl = document.getElementById('posts');
    albumsDivEl = document.getElementById('albums');
    loadButtonEl = document.getElementById('load-users');
    loadButtonEl.addEventListener('click', onLoadUsers);
});