const btnAnalisar = document.getElementById("btnAnalisar");
const spinner = document.getElementById("spinner");
const iconeBotao = document.getElementById("iconeBotao");
const mensagem = document.getElementById("mensagem");

// Preenche automaticamente a data atual
document.getElementById("data").value =
    new Date().toISOString().split("T")[0];

btnAnalisar.addEventListener("click", analisarCompra);

function mostrarMensagem(texto, tipo) {

    mensagem.textContent = texto;
    mensagem.className = `alert alert-${tipo} mb-4`;

}

function esconderMensagem() {

    mensagem.className = "alert d-none";
    mensagem.textContent = "";

}

async function analisarCompra() {

    esconderMensagem();

    const descricao = document.getElementById("descricao").value.trim();
    const valor = Number(document.getElementById("valor").value);
    const departamento = document.getElementById("departamento").value;

    document.getElementById("resultado").classList.add("d-none");

    if (descricao === "") {
        mostrarMensagem("Informe a descrição da compra.", "warning");
        return;
    }

    if (valor <= 0) {
        mostrarMensagem("Informe um valor estimado maior que zero.", "warning");
        return;
    }

    if (departamento === "") {
        mostrarMensagem("Selecione um departamento.", "warning");
        return;
    }

    btnAnalisar.disabled = true;

    spinner.classList.remove("d-none");
    iconeBotao.classList.add("d-none");

    try {

        const response = await fetch("/api/compras/analisar", {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                descricao: descricao,
                valorEstimado: valor,
                departamento: departamento
            })

        });

        if (!response.ok) {
            throw new Error();
        }

        const resultado = await response.json();

        document.getElementById("categoria").textContent = resultado.categoria;
        document.getElementById("prioridade").textContent = resultado.prioridade;
        document.getElementById("risco").textContent = resultado.risco;
        document.getElementById("justificativa").textContent = resultado.justificativa;
        document.getElementById("sugestao").textContent = resultado.sugestao;

        document.getElementById("resultado").classList.remove("d-none");

        mostrarMensagem("Análise realizada com sucesso.", "success");

        window.scrollTo({
            top: document.body.scrollHeight,
            behavior: "smooth"
        });

    }
    catch {

        mostrarMensagem(
            "Não foi possível realizar a análise da compra. Verifique se a API está em execução.",
            "danger");

    }
    finally {

        btnAnalisar.disabled = false;

        spinner.classList.add("d-none");
        iconeBotao.classList.remove("d-none");

    }

}