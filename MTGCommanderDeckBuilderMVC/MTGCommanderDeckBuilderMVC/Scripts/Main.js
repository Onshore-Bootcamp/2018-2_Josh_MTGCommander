function confirmUpdate() {
    if (confirm("Is this information correct?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmDelete() {
    if (confirm("Are you sure?")) {
        return true;
    } else {
        return false;
    }
}