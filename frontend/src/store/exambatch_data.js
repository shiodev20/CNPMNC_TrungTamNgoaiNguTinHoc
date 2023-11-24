import axios from "axios";
const exambatch = {
  namespaced: true,
  state: {
    exambatch: null,
    detail_embatch: null,
    data_post: {
      id: null,
      name: null,
      exam_id: null,
      status: true,
      exam_data: [],
    },
    err: {
      name: null,
      exam_id: null,
      exam_data: [],
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.exambatch = value;
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.exambatch.filter((e) => e.id == id);
    },
    getbyidexam: (state) => (id) => {
      return state.exambatch.filter((e) => e.exam_id == id);
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/exambatch");
      commit("set_value", data.data);
    },
    async create(context, data) {
      await axios.post("http://localhost:3000/exambatch", data);
    },
    async update({ commit, state }, data) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/exambatch/" + state.data_post.id,
        data
      );
      const datas = await axios.get("http://localhost:3000/exambatch");
      commit("set_value", datas.data);
    },
    async delete(context, id) {
      await axios.delete("http://localhost:3000/exambatch/" + id);
      const data = await axios.get("http://localhost:3000/exambatch");
      context.commit("set_value", data.data);
    },
  },
};
export default exambatch;
