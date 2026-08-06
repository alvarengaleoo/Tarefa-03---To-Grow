const btnAnalisar = document.getElementById("btnAnalisar");
const spinner = document.getElementById("spinner");
const iconeBotao = document.getElementById("iconeBotao");
const mensagem = document.getElementById("mensagem");

document.getElementById("data").value =
    new Date().toISOString().split("T")[0];

document.addEventListener("DOMContentLoaded", carregarConfiguracoes);

btnAnalisar.addEventListener("click", analisarCompra);

async function carregarConfiguracoes() {

    try {

        const response = await fetch("/api/configuracao");

        if (!response.ok)
            return;

        const config = await response.json();

        document.getElementById("modeloIa").textContent = config.modelo;
        document.getElementById("temperaturaIa").textContent = config.temperatura;
        document.getElementById("tokensIa").textContent = config.maxTokens;

    }
    catch (erro) {

        console.error("Erro ao carregar configurações:", erro);

    }

}

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

    const cardResultado = document.getElementById("resultado");

    cardResultado.classList.add("d-none");

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
                descricao,
                valorEstimado: valor,
                departamento
            })

        });

        const resultado = await response.json();

        if (!response.ok) {
            throw new Error(resultado.erro || "Erro ao realizar a análise.");
        }

        document.getElementById("categoria").textContent = resultado.categoria;
        document.getElementById("prioridade").textContent = resultado.prioridade;
        document.getElementById("risco").textContent = resultado.risco;
        document.getElementById("justificativa").textContent = resultado.justificativa;
        document.getElementById("sugestao").textContent = resultado.sugestao;

        cardResultado.classList.remove("d-none");

        mostrarMensagem("Análise realizada com sucesso.", "success");

    }
    catch (erro) {

        console.error(erro);

        mostrarMensagem(
            erro.message || "Não foi possível realizar a análise da compra.",
            "danger");

    }
    finally {

        btnAnalisar.disabled = false;

        spinner.classList.add("d-none");
        iconeBotao.classList.remove("d-none");

    }

}